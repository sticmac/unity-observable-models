using UnityEngine;

namespace Sticmac.ObservableModels {
    [CreateAssetMenu(fileName = "<%= Type %> Model", menuName = "Observable Models/<%= Type %>", order = 0)]
    public class <%= Type %>ObservableModel : ObservableModel<<%= TypeGeneric %>> {}
}