# Working
项目技术栈：asp.net core mvc、vue、bootstrap、jQuery、dapper、mysql  
对asp.net core mvc技术栈的技术总结，以这个项目为基础，摸索出一套适合自己的后端开发最佳实践  
最佳实践  
!、Repository层是单表操作，Service层是业务逻辑处理  
2、使用Sikiro.Dapper.Extension库，采用lambda表达式操作数据库，减少sql语句的编写，代码逻辑清晰。没有使用复杂的sql语句  
