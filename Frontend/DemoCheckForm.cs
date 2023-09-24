using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using startdemos_plus.Backend;
using startdemos_plus.Backend.DemoChecking;
using startdemos_plus.Utils;
using static startdemos_plus.Utils.Helpers;


namespace startdemos_plus.Frontend
{
    public partial class DemoCheckForm : UserControl
    {
        private ContextMenu _listChecksMenu = new ContextMenu();
        private ContextMenu _listCheckConditionsMenu = new ContextMenu();
        private ContextMenu _listActionMenu = new ContextMenu();

        private List<UIDemoCheckProfile> _profiles = new List<UIDemoCheckProfile>();

        private int __curProfileIndex = -1;
        private int _curProfileIndex 
        { 
            get => __curProfileIndex;
            set
            {
                __curProfileIndex = value;
                Program.Checks = _selectedProfile?.Checks ?? new List<DemoCheck>();
            }
        }
        private UIDemoCheckProfile _selectedProfile => _profiles.ElementAtOrDefault(_curProfileIndex);
        private int _curCheckIndex = -1;
        private DemoCheck _selectedCheck => _selectedProfile?.Checks.ElementAtOrDefault(_curCheckIndex);

        private bool _curCheckDirty = false;
        private bool _suspendDirtyCheck = false;
        public void CheckDirty()
        {
            if (!_suspendDirtyCheck && _curCheckDirty && _curCheckIndex != -1)
            {
                if (MessageBox.Show("Save changes to check?", "startdemos+ | Check Editor", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    butCheckApply_Click(null, null);
            }
            _curCheckDirty = false;
        }

        public DemoCheckForm()
        {
            InitializeComponent();

            Program.Settings.AddSetting
            (
                "checks-cur_selected_profile",
                (string e) =>
                {
                    var f = _profiles.FirstOrDefault(x => x.Name == e);
                    if (f != null)
                        cmbProfiles.SelectedIndex = _profiles.IndexOf(f);
                },
                () => _selectedProfile?.Name ?? ""
            );

            #region CHECK LIST DROPDOWN
            {
                var cmsAdd = new MenuItem("Add");
                cmsAdd.Click += (s, e) =>
                {
                    if (cmbProfiles.SelectedIndex == -1)
                        return;

                    string name = new UniqueNamePrompt().Show("Check", _selectedProfile.Checks.Select(x => x.Name).ToList());
                    if (name == null)
                        return;

                    _profiles[_curProfileIndex].Checks.Add(new DemoCheck(name));
                    FillCheckList();
                };
                _listChecksMenu.MenuItems.Add(cmsAdd);

                var cmsDel = new MenuItem("Delete Selected");
                cmsDel.Click += (s, e) =>
                {
                    if (_selectedCheck == null)
                        return;

                    _selectedProfile.Checks.RemoveAt(_curCheckIndex);
                    listChecks.Rows.RemoveAt(_curCheckIndex);
                };
                _listChecksMenu.MenuItems.Add(cmsDel);
                listChecks.MouseUp += (s, e) =>
                {
                    if (e.Button == MouseButtons.Right)
                        _listChecksMenu.Show(listChecks, e.Location);
                };
            }
            #endregion

            #region CONDITION LIST DROPDOWN
            {
                var cmsAdd = new MenuItem("Add");
                cmsAdd.Click += (s, e) =>
                {
                    if (_selectedCheck == null) return;
                    listCheckConditions.Rows.Add(
                        DemoCheckVariable.MapName.GetDescription(),
                        "",
                        false
                        );
                };
                _listCheckConditionsMenu.MenuItems.Add(cmsAdd);

                var cmsDel = new MenuItem("Delete Selected");
                cmsDel.Click += (s, e) =>
                {
                    if (_selectedCheck == null) return;
                    if (listCheckConditions.SelectedRows.Count == 0) return;

                    listCheckConditions.Rows.RemoveAt(listCheckConditions.SelectedRows[0].Index);
                };
                _listCheckConditionsMenu.MenuItems.Add(cmsDel);
                listCheckConditions.MouseUp += (s, e) =>
                {
                    if (e.Button == MouseButtons.Right)
                        _listCheckConditionsMenu.Show(listCheckConditions, e.Location);
                };
            }
            #endregion

            #region ACTION LIST DROPDOWN
            {
                var cmsAdd = new MenuItem("Add");
                cmsAdd.Click += (s, e) =>
                {
                    if (_selectedCheck == null) return;
                    listActions.Rows.Add(
                        DemoCheckActionType.StartDemoTime.GetDescription(),
                        ""
                        );
                };
                _listActionMenu.MenuItems.Add(cmsAdd);

                var cmsDel = new MenuItem("Delete Selected");
                cmsDel.Click += (s, e) =>
                {
                    if (_selectedCheck == null) return;
                    if (listActions.SelectedRows.Count == 0) return;

                    listActions.Rows.RemoveAt(listActions.SelectedRows[0].Index);
                };
                _listActionMenu.MenuItems.Add(cmsDel);
                listActions.MouseUp += (s, e) =>
                {
                    if (e.Button == MouseButtons.Right)
                        _listActionMenu.Show(listActions, e.Location);
                };
            }
            #endregion

            listCheckConditions.DefaultValuesNeeded += (s, e) => 
            {
                e.Row.Cells[0].Value = DemoCheckVariable.MapName.GetDescription();
                e.Row.Cells[1].Value = "";
                e.Row.Cells[2].Value = false;
            };

            listActions.DefaultValuesNeeded += (s, e) =>
            {
                e.Row.Cells[0].Value = DemoCheckActionType.StartDemoTime.GetDescription();
                e.Row.Cells[1].Value = "";
            };

            cmbProfiles.SelectedIndexChanged += (s, e) => 
            {
                CheckDirty();
                _curProfileIndex = cmbProfiles.SelectedIndex;
                FillCheckList();
            };
            listChecks.SelectionChanged += (s, e) =>
            {
                CheckDirty();
                _curCheckIndex = listChecks.SelectedRows.Count == 0 ? -1 : listChecks.SelectedRows[0].Index;
                FillCheckInfo();
            };

            boxCheckName.TextChanged += (s, e) => _curCheckDirty = _suspendDirtyCheck ? false : true;

            listCheckConditions.CellValueChanged += (s, e) => _curCheckDirty = _suspendDirtyCheck ? false : true;
            listCheckConditions.RowsAdded += (s, e) => _curCheckDirty = _suspendDirtyCheck ? false : true;
            listCheckConditions.RowsRemoved += (s, e) => _curCheckDirty = _suspendDirtyCheck ? false : true;

            listActions.CellValueChanged += (s, e) => _curCheckDirty = _suspendDirtyCheck ? false : true;
            listActions.RowsAdded += (s, e) => _curCheckDirty = _suspendDirtyCheck ? false : true;
            listActions.RowsRemoved += (s, e) => _curCheckDirty = _suspendDirtyCheck ? false : true;

            listChecks.RowsAdded += (s, e) => Globals.Events.ChecksModified?.Invoke(null, null);
            listChecks.RowsRemoved += (s, e) => Globals.Events.ChecksModified?.Invoke(null, null);

            boxCheckName.Click += (s, e) =>
            {
                if (_selectedProfile == null || _selectedCheck == null)
                    return;

                string name = new UniqueNamePrompt().Show("Check", _selectedProfile.Checks.Select(x => x.Name).ToList(), boxCheckName.Text);
                if (name != null)
                    boxCheckName.Text = name;
            };

            LoadSettings();

            FillCheckList();
            FillCheckInfo();
        }

        public void Exit()
        {
            CheckDirty();
            WriteSettings();
        }

        private void FillCheckInfo()
        {
            _suspendDirtyCheck = true;
            listCheckConditions.Rows.Clear();
            listActions.Rows.Clear();

            if (_curCheckIndex == -1)
            {
                boxCheckName.Text = "";
                gCheckSettings.Enabled = false;
            }
            else
            {
                gCheckSettings.Enabled = true;
                boxCheckName.Text = _selectedCheck.Name;

                _selectedCheck.Conditions.ForEach(x =>
                {
                    listCheckConditions.Rows.Add(
                        x.Variable.GetDescription(),
                        x.Condition,
                        x.Not);
                });

                _selectedCheck.Actions.ForEach(x =>
                {
                    listActions.Rows.Add(
                        x.Type.GetDescription(),
                        x.Params);
                });
            }
            _suspendDirtyCheck = false;
        }

        private void FillCheckList()
        {
            listChecks.Rows.Clear();

            if (_profiles.ElementAtOrDefault(cmbProfiles.SelectedIndex) != null)
                _selectedProfile.Checks.ForEach(x => listChecks.Rows.Add(x.Name));

            if (listChecks.RowCount > 0)
                listChecks.Rows[0].Selected = true;
        }

        private void butAddProfile_Click(object sender, EventArgs e)
        {
            var name = new UniqueNamePrompt().Show("Profile", _profiles.Select(x => x.Name).ToList());
            if (name == null) return;

            CheckDirty();

            _profiles.Add(new UIDemoCheckProfile(name));
            cmbProfiles.Items.Add(name);
            cmbProfiles.SelectedIndex = cmbProfiles.Items.Count - 1;
        }
        private void butRemoveProfile_Click(object sender, EventArgs e)
        {
            cmbProfiles.Text = "";

            if (_selectedProfile == null)
                return;

            if (MessageBox.Show($"Are you sure you want to delete profile \"{_selectedProfile.Name}\"?", "startdemos+ | Delete Profile", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) != DialogResult.Yes)
                return;

            _suspendDirtyCheck = true;

            _profiles.Remove(_selectedProfile);
            cmbProfiles.Items.RemoveAt(_curProfileIndex);
            FillCheckList();

            _suspendDirtyCheck = false;
        }

        private void butCheckApply_Click(object sender, EventArgs e)
        {
            if (_selectedCheck == null)
                return;

            List<DemoCheckCondition> conditions = new List<DemoCheckCondition>();
            foreach (DataGridViewRow row in listCheckConditions.Rows)
            {
                DemoCheckVariable var = GetValueFromDescription<DemoCheckVariable>((string)row.Cells[0].Value);
                string cond = (string)row.Cells[1].Value;
                bool not = (bool)row.Cells[2].Value;

                conditions.Add(new DemoCheckCondition(var, cond, not));
            }
            List<DemoCheckAction> actions = new List<DemoCheckAction>();
            foreach (DataGridViewRow row in listActions.Rows)
            {
                DemoCheckActionType actType = GetValueFromDescription<DemoCheckActionType>((string)row.Cells[0].Value);
                string actParam = (string)row.Cells[1].Value;
                actions.Add(new DemoCheckAction(actType, actParam));
            }

            _profiles[_curProfileIndex].Checks[_curCheckIndex] = new DemoCheck(boxCheckName.Text, conditions.ToArray(), actions.ToArray());

            _curCheckDirty = false;
            listChecks.Rows[_curCheckIndex].Cells[0].Value = _selectedCheck.Name;

            Globals.Events.ChecksModified?.Invoke(null, null);
        }
    }
}
