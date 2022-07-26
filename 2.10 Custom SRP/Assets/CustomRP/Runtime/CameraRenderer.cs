using UnityEngine;
using UnityEngine.Rendering;

namespace CustomRP.Runtime
{
    public class CameraRenderer
    {
        private ScriptableRenderContext _context;
        private Camera _camera;
        private const string _bufferName = "Render Camera";

        private CommandBuffer _buffer = new CommandBuffer()
        {
            name = _bufferName
        };

        public void Render(ScriptableRenderContext context, Camera camera)
        {
            _context = context;
            _camera = camera;

            Setup();
            DrawVisibleGeometry();
            Submit();
        }

        private void Setup()
        {
            _buffer.BeginSample(_bufferName);
            ExecuteBuffer();
            _context.SetupCameraProperties(_camera);
        }

        private void Submit()
        {
            _buffer.EndSample(_bufferName);
            ExecuteBuffer();
            _context.Submit();
        }

        private void ExecuteBuffer()
        {
            _context.ExecuteCommandBuffer(_buffer);
            _buffer.Clear();
        }

        private void DrawVisibleGeometry()
        {
            _context.DrawSkybox(_camera);
        }
    }
}