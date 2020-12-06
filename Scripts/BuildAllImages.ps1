docker build -f ./WebApp/Dockerfile -t super_todo/web_app:0.1 .
docker build -f ./TodoTasks.Api/Dockerfile -t super_todo/tasks_api:0.1 .
docker build -f ./Users.Api/Dockerfile -t super_todo/users_api:0.1 .
docker build -f ./TodoTasks.EventHandler/Dockerfile -t super_todo/todotasks_eventhandler:0.1 .
docker build -f ./Notifications/Dockerfile -t super_todo/notifications:0.1 .
docker build -f ./Auth.Api/Dockerfile -t super_todo/auth_api:0.1 .