using UnityEngine;

namespace PathCreation.Examples
{
    [ExecuteInEditMode]
    public abstract class PathSceneTool : MonoBehaviour
    {
        public event System.Action onDestroyed;
        public PathCreator pathCreator;
        public CreateTracks createTracks;
        public bool autoUpdate = true;

        public VertexPath path {
            get {
                return createTracks.theVertexPath;
            }
        }

        public void TriggerUpdate() {
            PathUpdated();
        }


        protected virtual void OnDestroy() {
            if (onDestroyed != null) {
                onDestroyed();
            }
        }

        protected abstract void PathUpdated();
    }
}
