properties([
    disableConcurrentBuilds(),
    disableResume(),
])

podTemplate(inheritFrom: 'jnlp-pod', containers: [
    containerTemplate(name: 'kaniko', image: 'gcr.io/kaniko-project/executor:debug', alwaysPullImage: false, ttyEnabled: true, command: '/busybox/cat'),
    containerTemplate(name: 'gitversions', image: 'gittools/gitversion', ttyEnabled: true, command: 'cat'),
  ]) {

    node(POD_LABEL) {
        ansiColor('xterm') {
            stage('checkout') {
                checkout scm
            }

            stage('gitversions') {
                container('gitversions') {
                    sh """
                    /tools/dotnet-gitversion web01 -output buildserver
                    """
                }
            }

            stage('Build Image') {
                withCredentials([[$class: 'ZipFileBinding', credentialsId: 'config-json', variable: 'DOCKER_CONFIG']]) {
                    container('kaniko') {
                        sh """
                        MAJOR=\$(cat gitversion.properties | grep -w GitVersion_Major | cut -d '=' -f2-)
                        MINOR=\$(cat gitversion.properties | grep -w GitVersion_Minor | cut -d '=' -f2-)
                        COMMIT=\$(cat gitversion.properties | grep -w GitVersion_CommitsSinceVersionSource | cut -d '=' -f2-)
                        SHA=\$(cat gitversion.properties | grep -w GitVersion_ShortSha | cut -d '=' -f2-)
                        TAG=\$(echo "\$MAJOR.\$MINOR.\$COMMIT-\$SHA")

                        cd web01
                        /kaniko/executor --context dir://. --cache=false --insecure --skip-tls-verify --destination=azalaxdev/demos:web-demo\$TAG
                        """
                    }
                }
            }
        }
    }
}
