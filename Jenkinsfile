pipeline {
  agent any
  tools {
       //jdk 'JDK'
	   msbuild 'MSBUILD'
       //dockerTool 'Docker'
   }
    stages {
        stage ('Initialize') {
            steps {
                bat '''
                    echo "PATH = ${PATH}"
                    '''
            }
        }
		stage ('Restore Nuget') {
            steps {
			     bat 'C:/ProgramData/chocolatey/bin/nuget.exe restore SeleniumNUnitParam.sln'
                //bat 'C:/ProgramData/chocolatey/lib/NuGet.CommandLine/tools/nuget.exe restore SeleniumNUnitParam.sln'
            }
        }
		stage ('Build .Net') {
            steps {
                bat "\" SeleniumNUnitParam.sln /p:Configuration=Debug /p:Platform\"Any CPU\" /p:ProductVersion=0.10.15.${env.BUILD_NUMBER}"
            }
        }
		stage('Test: Unit Test'){
   steps {
     bat 'C:/Program Files (x86)/NUnit.org/nunit-console/nunit3-console.exe SeleniumNUnitParam/bin/Debug/SeleniumNUnitParam.dll'
     }
  }
  }
  }