using UnityEngine;

namespace Sticmac.ObservableModels {
    [CreateAssetMenu(fileName = "Vector2 Model", menuName = "Observable Models/Vector2", order = 0)]
    public class Vector2ObservableModel : ObservableModel<Vector2>
    {
        /// <summary>
        /// Creates a new instance of the model.
        /// </summary>
        /// <param name="initialValue">The initial value of the model.</param>
        /// <returns>The new instance.</returns>
        public static Vector2ObservableModel Create(Vector2 initialValue = default(Vector2))
        {
            var instance = CreateInstance<Vector2ObservableModel>();
            instance._initialValue = initialValue;
            instance.Value = initialValue;
            return instance;
        }
        
        public override string StringValue {
            get => Value.ToString();
            set
            {
                value = value.Replace("(","").Replace(")","");

                string[] temp;
                if (value.Contains(","))
                {
                    temp = value.Split(',');
                }
                else
                {
                    temp = value.Split(' ');
                }

                float x = float.Parse(temp[0]);
                float y = float.Parse(temp[1]);

                Value = new Vector2(x, y);
            }
        }
    }
}