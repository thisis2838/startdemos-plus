using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace startdemos_plus.Utils
{
    public class PictureButton : PictureBox
    {
        private (Image src, Image gray) _curState;

        public Dictionary<string, (Image src, Image gray)> States = new Dictionary<string, (Image src, Image gray)>();

        public PictureButton() 
        {
        }

        public void AddState(string state, Image img)
        {
            if (States.ContainsKey(state))
                return;

            States.Add(state, (img, img.ToGrayScale()));
        }
        public void SetState(string state)
        {
            if (!States.ContainsKey(state))
                return;

            if (States[state] == _curState)
                return;

            _curState = States[state];
            Image = (Image)(Enabled ? _curState.src : _curState.gray).Clone();
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);

            if (_curState.src == null)
                return;
            
            base.Image = (Image)(Enabled ? _curState.src : _curState.gray).Clone();
            this.Cursor = Enabled ? Cursors.Hand : Cursors.Default;
            this.Invalidate();
        }
    }
}
