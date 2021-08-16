# Semeter-Fall
semeter-fall-schdule
An ASP.Net web application with some features and contraints to create a colleage schdule.

> the more important rule, i follow in that project >> **(fat Model skinny Controller)**,
> So i create **MethodHandler** class to provide the **Controller** with all methods it needs.

#### fall-semester_schedule_creator has some Constraints
* choosing a course (to make a section).
* assign days & timings.
* assign professor based on the course (without exceeding teaching load or conflict the professor timings).
* assign classroom without conflict the classroom timings.

Some of those constraints get done by add to coloum to database like in **Professors** Table(Teachingload - Committo) if the **Committo** equal to **TeachingLoad** that professor doesn't show again etc... .


## Frameworks
* ASP.Net
* .Net FrameWork 4.7.2



## Dependancies
* bootstrap(ClientSide)
* jQuery(ClientSide)
* jQuery.Validation(ClientSide)
* Microsoft.AspNet.Mvc
* Microsoft.EntityFrameworkCore
* Microsoft.EntityFrameworkCore.Design
* Microsoft.EntityFrameworkCore.SqlServer
* Microsoft.EntityFrameworkCore.Tools
* Microsoft.Extensions.DependencyInjection
* Newtonsoft.Json
* Select.HtmlToPdf

## Features

* Cascading DropDown list According to Contraints.
* Bootstrap Fully Design.
* More dynamic webpage by Jquery.
* Convert and Download Table's webpage.
* Normlized Database with internal Procedures

## Design patterns
* Repository design pattern with interface and Generic Behavioral Container.

## OverView

![Alt Gif](https://github.com/Moglten/Semeter-Fall/blob/main/media%20related/bandicam%202021-08-16%2001-47-56-689.gif)


## Database 

### bak : [Database Bak](https://github.com/Moglten/Semeter-Fall/blob/main/semesterDb.bak)

### scheme :-

![Database scheme](https://github.com/Moglten/Semeter-Fall/blob/main/media%20related/digram111.png)
