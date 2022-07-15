using UnityEngine;
using UnityEngine.Rendering;

namespace CustomRP.Runtime
{
    public class CameraRenderer
    {
        private ScriptableRenderContext _context;
        private Camera _camera;

        public void Render(ScriptableRenderContext context, Camera camera)
        {
            _context = context;
            _camera = camera;

            DrawVisibleGeometry();
            Submit();
        }

        private void Submit()
        {
            _context.Submit();
        }

        private void DrawVisibleGeometry()
        {
            _context.DrawSkybox(_camera);
        }
    }
}