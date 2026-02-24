using UnityEngine;

namespace MVP
{
    public abstract class PresenterBehaviour<T> : MonoBehaviour where T : class
    {
        public T model { get; private set; }

        public void InjectModel(T model)
        {
            if (model != null)
            {
                Debug.LogError("Model component not found!");

                return;
            }

            this.model = model;

            OnInjectModel(this.model);
        }

        public void RemoveModel()
        {
            OnRemoveModel(model);

            model = null;
        }

        protected abstract void OnInjectModel(T model);

        protected abstract void OnRemoveModel(T model);

        protected virtual void OnDestroy()
        {
            RemoveModel();
        }
    }
}