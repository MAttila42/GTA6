using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA6Game.Helpers;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Collections;
using System.Collections.ObjectModel;

namespace GTA6Game.UserControls.Overlay.Modal
{
    public class ModalCollection : PropertyChangeNotifier, IEnumerable<Modal<object>>, INotifyCollectionChanged
    {
        public bool HasModals => Modals.Count != 0;

        private ObservableCollection<Modal<object>> Modals;

        public ModalCollection()
        {
            Modals = new ObservableCollection<Modal<object>>();
            CollectionChanged += OnModalCollectionChanged;
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add
            {
                ((INotifyCollectionChanged)Modals).CollectionChanged += value;
            }

            remove
            {
                ((INotifyCollectionChanged)Modals).CollectionChanged -= value;
            }
        }

        public Task<ModalResult<T>> OpenModal<T>(Modal<T> modal)
        {
            Modals.Add(modal as Modal<object>);
            TaskCompletionSource<ModalResult<T>> source = new TaskCompletionSource<ModalResult<T>>();

            void OnModalClosed(ModalResult<T> result)
            {
                source.SetResult(result);
                modal.Dispose();
                Modals.Remove(modal as Modal<object>);
            }

            modal.ModalClosed += OnModalClosed;
            return source.Task;
        }

        public IEnumerator<Modal<object>> GetEnumerator()
        {
            return ((IEnumerable<Modal<object>>)Modals).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Modals).GetEnumerator();
        }

        private void OnModalCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(HasModals));
        }
    }
}
