﻿using LearnApplication.Model.Command.Interface;
using LearnApplication.Model.Interface;

namespace LearnApplication.Model.Command
{

    public sealed class DecreaseRepetitionsCommand(Subject subject, ICardQuestion? question=null) : ICommand
    {
        private readonly Subject _subject = subject;
        private readonly ICardQuestion? _question = question;
        public void Execute()
        {
            _subject.RepetitionsQuestionsCount--;
            if( question is not null)
                _subject?.UpdateDbEvent?.Invoke(_question, EventArgs.Empty);
        }

    }
}
