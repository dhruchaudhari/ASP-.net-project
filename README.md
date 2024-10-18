Project title : FoodShoppingCart

Purpose : The purpose of this food shopping project is to provide an online platform where users can create accounts, browse available food items, add them to a cart, and place orders seamlessly. The admin has control over managing orders, updating their status, and adding new food items to the platform, ensuring efficient management of both user interactions and inventory.

Instructions for running the project : Follow the following steps.

1.Open command prompt. Go to a directory where you want to clone this project. Use this command to clone the project.

git clone https://github.com/dhruchaudhari/ASP-.net-project.git

2.Go to the directory where you have cloned this project, open the directory FoodShoppingCart-Mvc. You will find a file with name FoodShoppingCartMvc.sln. Double click on this file and this project will be opened in Visual Studio.

3.Open appsettings.json file and update connection string

"ConnectionStrings": {
  "conn": "data source=your_server_name;initial catalog=MovieStoreMvc; integrated security=true;encrypt=false"
}

4.Delete Migrations folder.

5.Open Tools > Package Manager > Package manager console

6.Run these 2 commands (works only with Visual studio)

  add-migration init

  update-database
7.Now you can run this project.
