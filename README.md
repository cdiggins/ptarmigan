# Ptarmigan

Ptarmigan is a framework for developing Windows well-structured and robust desktop applications. It does 
this by providing a solid software architecture with open-source implementations of common 
low-level infrastructure features.

Ptarmigan provides a rich set of functionality out of the box and provides proper decoupling of 
the UI and data model. This means all features can run without UI, greatly simplifying testing and 
development. 

# Background

Many people, especially non-programmers, take for granted just how much functionality is expected and 
required to develop a successful commercial desktop application for Windows. There is a set of features 
that virtually every non-trivial commercial package ends up having to implement, and to be honest, 
it hasn't changed much in the last 25 years. Indeed there are many resources online and open-source 
libraries to help accelerate this process, and modern tools and languages are better than ever. 

The key to success in software development is anticipating all of the required functionality and putting 
it together in such a way that the features work together to speed up development, rather than slowing it down. 

There are no great insights or secrets, but it does take a lot of practice to get it right. Luckily there are 
enough people that have been doing for a long time, that a number of architectural and coding patterns 
have been identified that can make application development much faster. 

This project is about capturing some of those patterns in one place and making them reusable building 
blocks, so that both you and I can more quickly get to the place we want to be: implementing the fun and 
useful featurs and workflows our customers need and want. 

# How to use it

Ptarmigan comes with a demo application that provides user facing functionality that demonstrates many of the 
features implemented for the purpose of debugging, profiling, instrumentation, and testing. 

You can take this application and build from it, or use the libraries as you want. 

## Do you require additional documentation, training, support, custom features, integration, or access to the road-map? 

We provide very affordable packages for users who want commercial grade support. 
Reach out to me at cdiggins@gmail.com to discuss the details. 

# Why open-source

This will be a better product because everyone can validate the source code and suggest improvements. 
It is released under a commercially friendly (MIT License) so you can use this code and libraries in your 
application with no requirement of attribution. 

# Features under Development

UI
* Layout
* Remembering position and sizing of dialogs
* Common dialog implementation 
* Window Docking 
* Automatic UI (property sets, configuration)
* Auto-complete
* Multi-Monitor Support  
* Dismissable dialogs
* Text Entry Memory
* Resolution resize support
* Window finding 
* Drag and Drop
* Cut and Paste

Configuration
* First Run
* Environment Variables
* Settings files
* Configuration editor 

User Management
* Account login 
* Integration with zero Auth
* Windows user profile querying 

Batch Processing
* Command-Line Arguments
* Headless / Quiet mode 

Extensibility
* Scripting language support via Roslyn 
* Configuration language
* Plugins
* API Calls 
* Macro System
* API documentation generated as HTML from source

Data Management
* Business data model management
* Notification of changes 
* Automated serialization 
* Auto-Save and Back-up
* Disaster Recovery 
* Data caching
* Support for databases 

File Management
* Recent Files
* Find files 
* Temporary file management 
* File Type 

Task Processing 
* Asynchronous execution 
* Progress Bar
* Cancellation
* Logging 
  
Help/Support
* Centralized error reporting
* Crash reporting 
* URL management
* Technical Support
* Licensing information
* Short and long tips
* Video tips 
* Recording 
* Tutorial mode
* Auto-generated help 
* Release notes generated from Git
* Version Information

Command Management
* Connectivity to keystroke / menu
* Text search
* Documentation 
* Instrumentation 

DevOps
* Building
* Commit				
* Git integration
* Support for multiple types of build 
  
QA
* Test tools 
* Performance Profiling
* Memory usage graphing
* Hidden features
* Test Data Management
* Log Bug (Internal/External)

Branding
* Logo
* Icon application
* Icon with file types
* File Association
* File Preview

Instrumentation, Analytics, Metrics
* Logging - debugging, user 
* Crash Information
* Windows event system 
* Windows Log file
* Profiling
  
Features
* Screenshotting
* Video Record
* Notifications
* Text Editor

Software Architecture 
* Decouples UI from domain model
* Feature set is testable without UI
* Leverages Domain Driven Design best practices
* Provides a centralized data management system inspired by Redux
* Support for client-server and peer-to-peer networks 

# Status: February 23, 2022 

These features exist across many different projects. I expect by end of March to have a working prototype of the system for you to play with.

In the mean-time I'd love to hear your suggestions and ideas for features to include at https://github.com/cdiggins/ptarmigan/issues.

