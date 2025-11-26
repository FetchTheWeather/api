pipeline {
    agent any

    environment {
        REGISTRY = 'docker.pietr.space'
        PROJECT = 'ftw'
        IMAGE_NAME_PREFIX = 'fetch-the-weather_backend'
    }

    stages {
        stage('Build and Push Images') {
            matrix {
                when {
                    anyOf {
                        branch 'main'
                        branch 'staging'
                    }
                }
                agent {
                    label 'debian12'
                }
                axes {
                    axis {
                        name 'SERVICE'
                        values  'gateway', 'logging', 'weather'
                    }
                }
                stages {
                    stage('Build and Push') {
                        steps {
                            script {
                                def dockerfile = "${SERVICE.capitalize()}.Dockerfile"
                                buildAndPush(SERVICE, dockerfile)
                            }
                        }
                    }
                }
            }
        }
    }

    post {
        always { cleanWs() }
        success { echo 'Build succeeded! Images pushed.' }
        failure { echo 'Build failed. Check logs.' }
    }
}

def buildAndPush(name, dockerfile) {
    def commit = sh(returnStdout: true, script: 'git rev-parse --short HEAD').trim()
    def branch = env.BRANCH_NAME ?: 'unknown'

    branch = branch.replaceAll('/', '-')

    def tag = "${branch}-${commit}-${BUILD_NUMBER}"
    def image = "${REGISTRY}/${PROJECT}/${IMAGE_NAME_PREFIX}_${name}:${tag}"

    echo "Building and pushing image: ${image}"

    docker.withRegistry("https://${REGISTRY}", 'ftw_harbor') {
        def img = docker.build(
            "${PROJECT}/${IMAGE_NAME_PREFIX}_${name}:${tag}",
            "-f ${dockerfile} ."
        )
        img.push()

        if (branch == 'main') {
            echo "Pushing :latest tag"
            img.push('latest')
        } else {
            echo "Pushing :staging tag"
            img.push('staging')
        }
    }
}
