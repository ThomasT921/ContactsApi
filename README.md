Contacts API

Run via IIS Express: 
BASE URL: https://localhost:44393

Controller for Actions is getgontacts

Methods in getContacts include:

GetContactsList: Returns a contacts info view model to populate drop downs and grid
UpdateContact: Take an Id and a ContactItemViewModel and updates a single contact
AddContact: Takes a ContactItemViewModel and adds it to the collection in the DataAccessLayer
DeleteContact: Takes an Id and removes it
