properties([
    disableConcurrentBuilds(),
    disableResume(),
])

podTemplate(inheritFrom: 'jnlp-pod', containers: [
    containerTemplate(name: 'kaniko', image: 'gcr.io/kaniko-project/executor:debug', alwaysPullImage: false, ttyEnabled: true, command: '/busybox/cat'),
  ]) {

    node(POD_LABEL) {
        ansiColor('xterm') {
            stage('checkout') {
                checkout scm
            }
            stage('Build Image') {
                withCredentials([[$class: 'ZipFileBinding', credentialsId: 'config-json', variable: 'DOCKER_CONFIG']]) {
                    container('kaniko') {
                        sh """
                        cd web01
                        /kaniko/executor --context dir://. --cache=false --insecure --skip-tls-verify --destination=azalaxdev/demos:web-demo1.1
                        """
                    }
                }
            }
        }
    }
}
