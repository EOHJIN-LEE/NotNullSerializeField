using UnityEngine;

namespace JinStudio.NotNull
{
    public class TestForNotNullSerializeField : MonoBehaviour
    {
        [SerializeField] [NotNull]
        private Camera mainGameCamera;

        [NotNull]
        public Transform playerSpawnPoint;
        
        [NotNull]
        private Transform hiddenPoint;
        

        public int gameLevel = 1;
        [SerializeField] private float speedMultiplier = 1.0f;
    }
}