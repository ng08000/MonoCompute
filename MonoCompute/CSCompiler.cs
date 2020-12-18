using System;
using System.Collections.Generic;
using System.Text;

namespace MonoCompute
{
    public class CSCompiler
    {
        public static ComputeShaderResource Compile(string path)
        {
            Dictionary<string, byte[]> byteCodes = new Dictionary<string, byte[]>();

            using (System.IO.StreamReader file = new System.IO.StreamReader(path))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    if (line.Contains("#pragma kernel "))
                    {
                        var kernel = System.Text.RegularExpressions.Regex.Split(line, "#pragma kernel ")[1];
                        var result = SharpDX.D3DCompiler.ShaderBytecode.CompileFromFile(path, kernel, "cs_5_0");
                        byteCodes.Add(kernel, result.Bytecode);
                    }
                }
            }
            return new ComputeShaderResource(byteCodes);
        }
    }
}
