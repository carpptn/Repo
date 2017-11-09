PUConnector. Integracja z PayU - Biblioteki i aplikacje integracyjne .NET / C#

PUConnector to pakiet aplikacji oraz bibliotek programistycznych dla platformy .NET umożliwiających szybką, bezpieczną i profesjonalną integrację z wiodącym operatorem płatności internetowych PayU.

Pakiet PUConnector dzieli się na dwie części:


a.)    PUConnector Libraries – zestaw bibliotek programistycznych dla platformy .NET umożliwiających integrację z PayU.

PUConnector Libraries składa się z:

- PUConnector.Api – główna biblioteka implementująca komunikację z PayU. Umożliwia wysyłanie żądań (utworzenia nowego zamówienia, anulowania zamówienia, itd.) i odbieranie odpowiedzi na owe żądania od systemu PayU. Żądania jak i odpowiedzi przekazywane są za pomocą modelu obiektowego opisanego w punkcie 7 dokumentacji "Model danych API".

- PUConnector.Notifications.Service – biblioteka umożliwiająca odbiór powiadomień o zmianach stanów/statusów transakcji od PayU.

- PUConnector.OrdersManager – biblioteka pozwalająca rejestrować w bazie danych wszystkie zamówienia, zwroty oraz powiadomienia a także wszystkie związane z nimi żądania i odpowiedzi przesłane i odebrane do / z PayU. PUConnector zawiera gotową strukturę bazy danych dla silników baz danych firmy Microsoft: SQL Server oraz SQL CE.

- PUConnector.SoapProxy.Service – biblioteka umożliwiająca korzystanie z całej funkcjonalności PUConnector (a zarazem PayU API) za pomocą protokołu SOAP 1.1 / SOAP 1.2.
 
- PUConnector.Db.Repository – biblioteka pozwalająca na pełny dostęp do bazy danych zamówień, zwrotów oraz powiadomień za pomocą biblioteki ORM PetaPoco.

- PUConnector.Db – projekt bazy danych przechowującej zamówienia, zwroty oraz powiadomienia dla silników baz danych firmy Microsoft: SQL Server oraz SQL CE.

- PUConnector.Commons – biblioteka wspólna wykorzystywana przez pozostałe biblioteki PUConnector. Zawiera strukturę obiektowego modelu danych API (opisanego szerzej w punkcie 7 dokumentacji "Model danych API"), funkcjonalność obsługi i logowania błędów oraz kontroli bezpieczeństwa transmisji.

b.)    PUConnector Toolkit – zestaw aplikacji do integracji z PayU.

PUConnector Toolkit składa się z:

- PUConnector.Api.Cmd – aplikacja konsolowa uruchamiana z systemowego wiersza poleceń. Aplikacja umożliwia wysyłanie żądań (utworzenia nowego zamówienia, anulowania zamówienia, itd.) i odbieranie odpowiedzi na owe żądania od systemu PayU.  Żądania jak i odpowiedzi przesyłane są w formacie JSON. Struktura żądań i odpowiedzi opisana została w punkcie 7 dokumentacji "Model danych API". Aplikacja udostępnia funkcjonalność biblioteki PUConnector.Api.

- PUConnector.Notifications.Service – usługa (WCF) web (hostowana na serwerze web IIS) umożliwiająca odbiór od PayU powiadomień o zmianach stanów/statusów zamówień.

- PUConnector.Notifications.Host – usługa Windows Service umożliwiająca odbiór od PayU powiadomień o zmianach stanów/statusów zamówień. PUConnector.Notifications.Host stanowi alternatywę dla PUConnector.Notifications.Service kiedy nie możemy bądź nie chcemy używać serwera IIS. Aplikacja stanowi środowisko uruchomieniowe dla biblioteki PUConnector.Notifications.Service.

- PUConnector.SoapProxy.Service – usługa (WCF) WebService (hostowana na serwerze web IIS) umożliwiająca korzystanie z całej funkcjonalności PUConnector (a zarazem PayU API) za pomocą protokołu SOAP 1.1 / SOAP 1.2. SoapProxy udostępnia za pomocą usługi WebServices całą funkcjonalność bibliotek PUConnector.Api (wysyłanie żądań i odbieranie odpowiedzi od PayU) oraz PUConnector.OrdersManager (rejestrowanie w bazie danych żądań, odpowiedzi i całej komunikacji z PayU).

- PUConnector.SoapProxy.Host – usługa Windows Service umożliwiająca korzystanie z całej funkcjonalności PUConnectora (a zarazem PayU API) za pomocą protokołu SOAP 1.1 / SOAP 1.2. PUConnector.SoapProxy.Host stanowi alternatywę dla PUConnector.SoapProxy.Service kiedy nie możemy bądź nie chcemy używać serwera IIS. Aplikacja stanowi środowisko uruchomieniowe dla biblioteki PUConnector.SoapProxy.Service.

- PUConnector.Api.UI – aplikacja Windows z graficznym interfejsem użytkownika pozwalająca w prosty i wygodny sposób testować, analizować i optymalizować komunikację z PayU.
