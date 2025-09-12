using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions
{

    [Category("Input (New System)")]
    public class InputActive : ActionTask
    {

        [RequiredField]
        public bool enable;

        protected override void OnExecute() { Do(); }

        void Do()
        {
            if (enable)
                GameObject.FindObjectOfType<MyInput>().InputEnable();
            else
                GameObject.FindObjectOfType<MyInput>().InputDisable();
            EndAction();
        }
    }
}