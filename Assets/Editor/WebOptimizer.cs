using UnityEditor;
using UnityEditor.Build;
using UnityEditor.WebGL;

namespace Editor
{
    public class WebOptimizer
    {
        [MenuItem("Example/Optimize")]
        public static void Optimize()
        {
            var namedBuildTarget = NamedBuildTarget.WebGL;
            var buildOptions = BuildOptions.CompressWithLz4HC;

            // Set IL2CPP code generation to Optimize Size 
            PlayerSettings.SetIl2CppCodeGeneration(namedBuildTarget,
                Il2CppCodeGeneration.OptimizeSize);

            // Set the Managed Stripping Level to High
            PlayerSettings.SetManagedStrippingLevel(namedBuildTarget,
                ManagedStrippingLevel.High);

            // Strip unused mesh components           
            PlayerSettings.stripUnusedMeshComponents = true;

            // Enable data caching
            PlayerSettings.WebGL.dataCaching = true;

            // Set the compression format to Brotli
            PlayerSettings.WebGL.compressionFormat = WebGLCompressionFormat.Brotli;

            // Deactivate exceptions
            PlayerSettings.WebGL.exceptionSupport = WebGLExceptionSupport.None;

            // Deactivate debug symbols
            PlayerSettings.WebGL.debugSymbolMode = WebGLDebugSymbolMode.Off;

            // Set Platform Settings to optimize for disk size (LTO)
            UserBuildSettings.codeOptimization = WasmCodeOptimization.DiskSizeLTO;
        }
    }
}