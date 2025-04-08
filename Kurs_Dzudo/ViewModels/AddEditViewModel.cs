using Avalonia.Controls;
using Kurs_Dzudo.Hardik.Connector.Date;
using ReactiveUI;
using System;
using System.Reactive;

namespace Kurs_Dzudo.ViewModels
{
    public class AddEditViewModel : ReactiveObject
    {
        private UkhasnikiDao _currentParticipant;

        public UkhasnikiDao CurrentParticipant
        {
            get => _currentParticipant;
            set => this.RaiseAndSetIfChanged(ref _currentParticipant, value);
        }

        public DateTimeOffset? SelectedDate
        {
            get => CurrentParticipant?.DateSorevnovaniy == default
                ? null
                : new DateTimeOffset(CurrentParticipant.DateSorevnovaniy.ToDateTime(TimeOnly.MinValue));
            set
            {
                if (CurrentParticipant != null)
                {
                    CurrentParticipant.DateSorevnovaniy = value.HasValue
                        ? DateOnly.FromDateTime(value.Value.DateTime)
                        : default;
                    this.RaisePropertyChanged();
                }
            }
        }

        public string WindowTitle => CurrentParticipant?.Name == null ? "Добавить участника" : "Редактировать участника";

        public ReactiveCommand<Unit, Unit> SaveCommand { get; set; }
        public ReactiveCommand<Unit, Unit> CancelCommand { get; set; }

        public AddEditViewModel()
        {
            CurrentParticipant = new UkhasnikiDao();
            SetupCommands();
        }

        public AddEditViewModel(UkhasnikiDao participant)
        {
            CurrentParticipant = participant;
            SetupCommands();
        }

        private void SetupCommands()
        {
            SaveCommand = ReactiveCommand.Create(() => { });
            CancelCommand = ReactiveCommand.Create(() => { });
        }
    }
}