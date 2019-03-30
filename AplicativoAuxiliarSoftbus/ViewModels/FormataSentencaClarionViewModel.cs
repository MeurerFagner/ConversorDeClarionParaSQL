using AplicativoAuxiliarSoftbus.Models;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace AplicativoAuxiliarSoftbus.ViewModels
{
    public class FormataSentencaClarionViewModel : BindableBase
    {
        private string _sentencaCalrion;
        public string SentencaCalrion
        {
            get { return _sentencaCalrion; }
            set {
                if (value != _sentencaCalrion)
                {
                    VariaveisClarion = ConversorDeSentenca.ExtrairVariaveisCalrionDeSentenca(value);
                    RaisePropertyChanged("VariaveisClarion");
                }

                SetProperty(ref _sentencaCalrion, value);
            }
        }

        private string _sentencaFormatada;
        public string SentencaFormatada
        {
            get { return _sentencaFormatada; }
            set { SetProperty(ref _sentencaFormatada, value); }
        }

        
        public ObservableCollection<VariavelClarion> VariaveisClarion { get; set; }
        public DelegateCommand ConverterSentencaCommand { get; private set; }
        public InteractionRequest<INotification> NotificationRequest { get; private set; }

        public FormataSentencaClarionViewModel()
        {
            VariaveisClarion = new ObservableCollection<VariavelClarion>();
            //SentencaCalrion = "Teste";
            NotificationRequest = new InteractionRequest<INotification>();
            ConverterSentencaCommand = new DelegateCommand(Convertersenteca);
            //ConverterSentencaCommand.ObservesProperty(() => SentencaCalrion);
            //ConverterSentencaCommand.ObservesProperty(() => VariaveisClarion);
            
        }

        private bool CanConverterSentenca()
        {
            if (string.IsNullOrWhiteSpace(_sentencaCalrion)) return false;
            if (VariaveisClarion.Count(p => string.IsNullOrWhiteSpace(p.Valor)) > 0) return false;
            return true;
        }

        private void Convertersenteca()
        {
            SentencaFormatada = ConversorDeSentenca.ConverteSentencaClarionParaSQL(SentencaCalrion, VariaveisClarion);
            NotificationRequest.Raise(new Notification
            {
                Title = "Resultado da Conversão",
                Content = SentencaFormatada
            }, r => {
                
                var result = MessageBox.Show("Copiar Sentença para area de transferencia?", "Copy to ClipBoard", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                    Clipboard.SetText(SentencaFormatada);
            });
        }



    }
}
