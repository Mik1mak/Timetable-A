# Timetable-A
Timetable-A to responsywna aplikacja Internetowa zbudowana przy pomocy Angulara 12 wraz z Bootstrapem 5 oraz ASP .NET Core API. 
Jej główną funkcjonajnością jest tworzenie i udostępnianie planów zajęć. Zajęcia znajdujące się w planie zorganizowane są w grupy,
wewnątrz których nie mogą się na siebie nakładać w czasie. Każda z grup może mieć przypisany swój własny kolor, którym będą zaznaczane
jej przedmioty. Jednocześnie wyświetlać można jedynie zajęcia z różnych grup pod warunkiem, że nie nakłądają się one na siebie.
Kombinacja zaznaczonych grup zapisywana i aktualizowana jest w adresie URL, dzięki czemu odświeżenie strony nie zmieni ustawień,
a udostępniony plan będzie otwierany w tej samej konfiguracji.
Każdy z planów lekcji można otworzyć w trybie do edycji lub tylko do odczytu. Za autoryzację odpowiada standard JSON Web Token.

Aplikacja Dostępna pod [tym](https://timetableappstatic.z22.web.core.windows.net) adresem.

[Demo w trybie edycji](https://timetableappstatic.z22.web.core.windows.net/login/?id=5&key=JGOrH3NOUO2EJhQT&returnUrl=%2F%3Fg%3D11%26g%3D29%26g%3D36%26g%3D19%26g%3D12)

[Demo w trybie do odczytu](https://timetableappstatic.z22.web.core.windows.net/login/?id=5&key=gYeymOKTh8qFFxES&returnUrl=%2F%3Fg%3D11%26g%3D29%26g%3D36%26g%3D19%26g%3D12)
