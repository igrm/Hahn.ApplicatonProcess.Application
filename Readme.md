# How to build and start the application 
 1. Make sure you have latest docker (on linux) / docker desktop (on windows) version installed.
 2. Navigate in terminal to Hahn.ApplicatonProcess.Application directory.
 3. Execute in terminal following command: `docker-compose build`
 4. Execute in terminal following command: `docker-compose start`
 5. Open in browser `http://localhost:8080` and you should see the input form, rolling logs are stored within `./Logs` directory.