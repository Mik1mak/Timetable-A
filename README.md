# Timetable-A
Timetable-A to responsywna aplikacja Internetowa zbudowana przy pomocy Angulara 12 wraz z Bootstrapem 5 oraz ASP .NET Core API. 
Jej główną funkcjonalnością jest tworzenie i udostępnianie planów zajęć. Zajęcia znajdujące się w planie zorganizowane są w grupy,
wewnątrz których nie mogą się na siebie nakładać w czasie. Każda z grup może mieć przypisany swój własny kolor, którym będą zaznaczane
jej przedmioty. Jednocześnie wyświetlać można jedynie zajęcia z różnych grup pod warunkiem, że nie nakładają się one na siebie.
Kombinacja zaznaczonych grup zapisywana i aktualizowana jest w adresie URL, dzięki czemu odświeżenie strony nie zmieni ustawień,
a udostępniony plan będzie otwierany w tej samej konfiguracji.
Każdy z planów lekcji można otworzyć w trybie do edycji lub tylko do odczytu. Autoryzajca wykonana jest zgodnie ze standardem JSON Web Token.

Aplikacja Dostępna pod [tym](https://timetablea.azurewebsites.net) adresem. 

[Demo w trybie edycji](https://timetablea.azurewebsites.net/login/?id=16&key=D4bgMO3IuX3J0jAq&returnUrl=%2F%3Fg%3D41%26g%3D40%26g%3D43)

[Demo w trybie do odczytu](https://timetablea.azurewebsites.net/login/?id=15&key=uGU1u0NrjCsCuwVk&returnUrl=%2F%3Fg%3D37%26g%3D34%26g%3D32%26g%3D35)
