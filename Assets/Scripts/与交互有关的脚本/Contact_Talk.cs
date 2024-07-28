using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NodeEditor.Resources
{
    public class Contact_Talk : InterActable
    {
        public GameObject PressIcon;//NN
        [Tooltip("对话脚本")] public Story story;
        private void Start()
        {
            if (!PressIcon)
                Debug.Log("PressIcon为空");
            else
                PressIcon.SetActive(false);
        }
        public override Intertype Active()
        {
            DialogUI.Instance.OpenOrClose();
            DialogUI.Instance.Story = story;
            return Intertype.Contact_Talk;
        }
        public override void CheckEnter()
        {
            PressIcon.SetActive(true);
            base.CheckEnter();
        }
        public override void CheckStay()
        {
            base.CheckStay();
        }
        public override void CheckExit()
        {
            PressIcon.SetActive(false);
            base.CheckExit();
        }
    }
}