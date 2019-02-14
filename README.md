# BugTraq

### Installation
If you don't already have then install node, npm and dotnet core

https://blog.teamtreehouse.com/install-node-js-npm-windows

https://dotnet.microsoft.com/download
```sh
$ cd [CloneFolder]/src/buqtraq.api
$ dotnet run
```
In another console/terminal window

```sh
$ cd [CloneFolder]/src/bugtraq.presentation
$ npm install
$ npm install local-web-server -g
$ ws -p 3000
```
In a browser go to ```http://localhost:3000``` and you should be presented with the BugTraq main page

### Add a bug

![Add A Bug](screenshots/new.jpg?raw=true "Add A Bug")

![Add A Bug](screenshots/newdetails.png?raw=true "Add A Bug")


### Edit/delete a bug

![Edit or delete](screenshots/editdelete.png?raw=true "Edit or delete a bug")

### Add users

![Add Users](screenshots/users.png?raw=true "Add Users")
