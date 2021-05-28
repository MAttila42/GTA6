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
    public class ModalCollection : PropertyChangeNotifier, IEnumerable<BaseModal>, INotifyCollectionChanged
    {
        public bool HasModals => Modals.Count != 0;

        private ObservableCollection<BaseModal> Modals;

        public ModalCollection()
        {
            Modals = new ObservableCollection<BaseModal>();
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
            Modals.Add(modal as BaseModal);
            TaskCompletionSource<ModalResult<T>> source = new TaskCompletionSource<ModalResult<T>>();

            void OnModalClosed(ModalResult<T> result)
            {
                source.SetResult(result);
                modal.Dispose();
                Modals.Remove(modal as BaseModal);
            }

            modal.ModalClosed += OnModalClosed;
            return source.Task;
        }

        public IEnumerator<BaseModal> GetEnumerator()
        {
            return ((IEnumerable<BaseModal>)Modals).GetEnumerator();
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
