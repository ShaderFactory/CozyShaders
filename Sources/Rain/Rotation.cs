using UnityEngine;

namespace ShaderFactory.DynamicSky
{
    public class Rotation : MonoBehaviour
    {
        public Vector3 rot = new Vector3(0, 10, 0);

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(rot * Time.deltaTime);
        }
    }
}
