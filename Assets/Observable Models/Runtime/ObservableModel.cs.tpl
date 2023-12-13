using UnityEngine;

namespace Sticmac.ObservableModels {
    [CreateAssetMenu(fileName = "<%= Type %> Model", menuName = "Observable Models/<%= Type %>", order = 0)]
    public class <%= Type %>ObservableModel : ObservableModel<<%= TypeGeneric %>>
    {
        /// <summary>
        /// Creates a new instance of the model.
        /// </summary>
        /// <param name="initialValue">The initial value of the model.</param>
        /// <returns>The new instance.</returns>
        public static <%= Type %>ObservableModel Create(<%= TypeGeneric %> initialValue = default(<%= TypeGeneric %>))
        {
            var instance = CreateInstance<<%= Type %>ObservableModel>();
            instance._initialValue = initialValue;
            instance.Value = initialValue;
            return instance;
        }
    }
}