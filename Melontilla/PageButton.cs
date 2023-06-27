﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Melontilla
{
    public class PageButton : GorillaPressableButton
    {
        public Action onPressed;

        public override void ButtonActivation()
        {
            base.ButtonActivation();

            onPressed();
        }
    }
}
