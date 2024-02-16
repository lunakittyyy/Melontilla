using System;

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
