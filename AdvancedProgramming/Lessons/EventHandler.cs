using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedProgramming.Lessons;

public static class EventHandler
{

    public class ButtonEventArgs : EventArgs
    {
        public string ButtonTest { get; }

        public ButtonEventArgs(string ButtonText)
        {
            this.ButtonTest = ButtonText;
        }
    }



    class Button
    {
        public event EventHandler<ButtonEventArgs>? OnClicked;
        
        public string ButtonText { get; set; }

        public Button (string ButtonText) => this.ButtonText = ButtonText;

        public void click()
        {
            OnClicked?.Invoke(this, new ButtonEventArgs(ButtonText));
        }

        public override string ToString()
        {
            return "this is button";
        }
    }


}
