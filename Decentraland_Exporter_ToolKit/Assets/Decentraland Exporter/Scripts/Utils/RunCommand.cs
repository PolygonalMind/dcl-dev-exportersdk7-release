
using System;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using UnityEngine.Windows;

namespace DCLExport
{
    public static class RunCommand
    {
        public static void DependencieCheck()
        {
#if UNITY_EDITOR_OSX

#else
            //DclExporter.clearDeps();

            var cmdNpm = "/k npm -v";
            ExecuteCommand(cmdNpm, true, false);
            var cmdNode = "/k node -v";
            ExecuteCommand(cmdNode, true, false);

            var cmdGLTF = "/k gltf-pipeline --version";
            ExecuteCommand(cmdGLTF, true, false);
            
            var cmdCLI = "/k dcl -v";
            ExecuteCommand(cmdCLI, true, false);
#endif
        }
        public static void DclInit(string path)
        {
#if UNITY_EDITOR_OSX

#else
            //string strCmdText;
            //strCmdText = string.Format("/k cd /d {0} & npx @dcl/sdk-commands init", path); //Command
            //System.Diagnostics.Process.Start("CMD.exe", strCmdText); //Start cmd process
            var cmd = string.Format("/k cd /d {0} & npx @dcl/sdk-commands init", path);
            ExecuteCommand(cmd);
#endif
        }
        public static void DclDeleteFolderContent(string path)
        {
#if UNITY_EDITOR_OSX

#else
            var cmd = string.Format("/c rd /s /q {0} & md {0}", path.Replace("/", "\\"));
            ExecuteCommand(cmd,false,false);
#endif
        }
        public static void DclUpdateSdk(string path)
        {
#if UNITY_EDITOR_OSX

#else
            var cmd = string.Format("/k cd /d {0} & npm i @dcl/sdk@latest", path);
            ExecuteCommand(cmd);
#endif
        }
        public static void DclUpdateCLI(string path)
        {
#if UNITY_EDITOR_OSX

#else
            var cmd = string.Format("/k cd /d {0} & npm install -g decentraland", path);
            ExecuteCommand(cmd);
#endif
        }
        public static void DclUninstallCLI(string path)
        {
#if UNITY_EDITOR_OSX

#else
            var cmd = string.Format("/k cd /d {0} & npm uninstall -g decentraland", path);
            ExecuteCommand(cmd);
#endif
        }

        public static void DclRunStart(string path, string type)
        {
#if UNITY_EDITOR_OSX

#else
            var cmd = string.Format("/k cd /d {0} & npm run start" + type, path);
            ExecuteCommand(cmd);
#endif
        }
        public static void GltfPipeline()
        {
#if UNITY_EDITOR_OSX

#else
            var cmd = "/k npm install -g gltf-pipeline";
            ExecuteCommand(cmd);
#endif
        }
        public static void UninstallGltfPipeline()
        {
#if UNITY_EDITOR_OSX

#else
            var cmd = "/k npm uninstall -g gltf-pipeline";
            ExecuteCommand(cmd);
#endif
        }

        // gtTF to Glb Conversion
        public static void gltfGLBexternal(string path, string name)
        {
#if UNITY_EDITOR_OSX
#else
            var cmd = string.Format("/C cd /d {0} & gltf-pipeline -i " + name + ".gltf -o " + name + ".glb -t ", path);
            ExecuteCommandGLTF(cmd, path, name);
#endif
        }
        public static void gltfGLBcompress(string path, string name)
        {
#if UNITY_EDITOR_OSX
#else
            var cmd = string.Format("/C cd /d {0} & gltf-pipeline -i " + name + ".gltf -o " + name + ".glb ", path);
            ExecuteCommandGLTF(cmd, path, name);
#endif
        }
        public static void deleteGLTFBIN(string path, string name)
        {
            var cmd = string.Format("/C cd /d {0} & DEL {1}.gltf & DEL {1}.bin", path, name);
            ExecuteCommand(cmd, false, false);
        }
        public static void deleteExternalTextures(string path)
        {
            var cmd2 = string.Format("/C cd /d {0} & rmdir /s /q gltfTextures", path + "/unity_assets");
            ExecuteCommand(cmd2, false, true);
        }



        // Execure commands
        public static void ExecuteCommandGLTF(string cmd, string path, string name)
        {
            var thread = new Thread(delegate ()
            {
                CommandGLTF(cmd, path, name);
            });
            thread.Start();
        }

        static void CommandGLTF(string cmd, string path, string name)
        {
            //UnityEngine.Debug.Log(String.Format("exec cmd {0}",cmd));
            string exe = "";
#if UNITY_EDITOR_OSX
            exe = "/bin/bash";
#else
            exe = "cmd.exe";

#endif
            var processInfo = new ProcessStartInfo(exe, cmd);
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = true;
            processInfo.Arguments = cmd;
            processInfo.FileName = exe;

            var process = new Process();
            process.StartInfo = processInfo;

            process.Start();
            process.WaitForExit();

            deleteGLTFBIN(path, name);

            process.Close();
        }
        public static void ExecuteCommand(string cmd, bool createOutput = false, bool createWindow = true)
        {
            var thread = new Thread(delegate ()
            {
                Command(cmd, createOutput, createWindow);
            });
            thread.Start();
        }

        static void Command(string cmd, bool createOutput, bool createWindow)
        {
            //UnityEngine.Debug.Log(String.Format("exec cmd {0}",cmd));
            string exe = "";
#if UNITY_EDITOR_OSX
            exe = "/bin/bash";
#else
            exe = "cmd.exe";

#endif
            var processInfo = new ProcessStartInfo(exe, cmd);
            processInfo.CreateNoWindow = !createWindow;
            processInfo.UseShellExecute = !createOutput;
            processInfo.Arguments = cmd;
            processInfo.FileName = exe;
            if(createOutput)
                processInfo.RedirectStandardOutput = true;

            var process = new Process();
            process.StartInfo = processInfo;

            if (createOutput)
                process.OutputDataReceived += (sender, a) => OutDataHandler(a.Data, cmd);

            process.Start();

            if (createOutput)
            {
                //DataHandler(process.StandardOutput.ReadLine(), cmd);
                //UnityEngine.Debug.Log(process.StandardOutput.ReadToEnd());
                process.BeginOutputReadLine();
            }

            process.WaitForExit();
            process.Close();
        }
        
        static void OutDataHandler(string value, string cmd)
        {
            if (value == null || value.Contains(":\\")) return;
            
            //UnityEngine.Debug.Log("Output Line: " + value);
            if (cmd.Contains("node -v"))
            {
                if(value.Split(".").Length == 3 && value.StartsWith("v"))
                {
                    DclExporter.nodeVersion = value;
                    DclExporter.nodeFound = true;
                    UnityEngine.Debug.Log("Found NodeJS, Version: " + value);
                }
                else if(!DclExporter.nodeFound)
                {
                    UnityEngine.Debug.LogError("¡NodeJS not Found!\nCheck your NodeJS version or install the last LTS if needed");
                }
                return;
            }
            if (cmd.Contains("npm -v"))
            {
                if(value.Split(".").Length == 3)
                {
                    DclExporter.npmVersion = value;
                    DclExporter.npmFound = true;
                    UnityEngine.Debug.Log("Found Npm, Version: " + value);
                }
                else if(!DclExporter.npmFound)
                {
                    UnityEngine.Debug.LogError("¡Npm not found!\nCheck your Npm version and install the last LTS if needed");
                }
                return;
            }
            if (cmd.Contains("gltf-pipeline --version"))
            {
                if (value.Split(".").Length == 3)
                {
                    DclExporter.gltfVersion = value;
                    DclExporter.gltfFound = true;
                    UnityEngine.Debug.Log("Found Gltf-Pipeline, Version: " + value);
                }
                else if(!DclExporter.gltfFound)
                {
                    UnityEngine.Debug.LogError("¡GLTF-Pipeline not found!\nYou can install GLTF-Pipeline with the button \"Install GLTF-Pipeline\"");
                }
                return;
            }
            if (cmd.Contains("dcl -v"))
            {
                if (value.Split(".").Length == 3)
                {
                    DclExporter.dclCliVersion = value;
                    DclExporter.dclCliFound = true;
                    UnityEngine.Debug.Log("Found Decentraland CLI, Version: " + value);
                }
                else if(!DclExporter.dclCliFound)
                {
                    UnityEngine.Debug.LogError("¡Decentraland CLI not found!\nYou can install Decentraland CLI with the button \"Update CLI\" in the Decentraland CLI section");
                }
                return;
            }
        }
    }
}

