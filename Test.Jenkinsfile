

pipeline {
    agent any

    environment {
        REPO_URL = 'https://github.com/MohamadAlturky/BPMNStorage.git'
        BRANCH = 'test'
        CREDENTIALS_ID = 'github'
    }

    stages {
        stage('Clone Repository') {
            steps {
                git branch: "${BRANCH}", url: "${REPO_URL}", credentialsId: "${CREDENTIALS_ID}"
            }
        }

        stage('Run Docker Compose') {
            steps {
                script {
                    sh "docker-compose -f docker-compose.IntegrationTests.yaml up --build"
                    sh "docker-compose -f docker-compose.UnitTests.yaml up --build"
                }
            }
        }
    }
}