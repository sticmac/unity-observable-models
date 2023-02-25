using UnityEngine;

namespace Sticmac.ObservableModel {
    [CreateAssetMenu(fileName = "<%= Type %> Model", menuName = "Observable Models/<%= Type %>", order = 0)]
    public class <%= Type %>ObservableModel : ObservableModel<<%= TypeGeneric %>> {}
}