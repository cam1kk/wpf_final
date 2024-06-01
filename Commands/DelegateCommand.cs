﻿using System;
using System.Windows.Input;

namespace wpf_final.Commands
{
    internal class DelegateCommand : Command
    {
        private static readonly Func<bool> defaultExecuteMethod = () => true;

        private Func<bool> canExecuteMethod;
        private readonly Action executeMethod;

        public DelegateCommand(Action executeMethod)
            : this(executeMethod, defaultExecuteMethod)
        { }

        public DelegateCommand(Action executeMethod, Func<bool> canExecuteMethod)
        {
            this.executeMethod = executeMethod;
            this.canExecuteMethod = canExecuteMethod;
        }

        public override void Execute()
        {
            executeMethod();
        }

        public override bool CanExecute()
        {
            return canExecuteMethod();
        }
    }
}
