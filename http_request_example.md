Those are example of http request for the API

Get all
curl -X GET \
  http://localhost:1234/api/values \
  -H 'Cache-Control: no-cache' \
  -H 'Postman-Token: 011afcac-aee8-4eef-9f82-72f99402590f'

Get by id
curl -X GET \
  http://localhost:1234/api/values/image/8 \
  -H 'Cache-Control: no-cache' \
  -H 'Postman-Token: 1e9b3f18-14d9-4b43-a30d-ce5aa47290c4'

Get by mail
curl -X GET \
  http://localhost:1234/api/values/email/john@chickenandco.com \
  -H 'Cache-Control: no-cache' \
  -H 'Postman-Token: 638f9b89-4d0d-4956-a4c4-dd0d6b8dc1ef'

Get by phone
curl -X GET \
  http://localhost:1234/api/values/image/8 \
  -H 'Cache-Control: no-cache' \
  -H 'Postman-Token: 81f67b5e-6327-428f-b8bd-61252ac2cdb9'

Insert Record
curl -X POST \
  http://localhost:1234/api/values \
  -H 'Cache-Control: no-cache' \
  -H 'Content-Type: application/json' \
  -H 'Postman-Token: c8d842d8-6d4c-44e9-99c3-a3de197c447c' \
  -d '{"Name":"John Doe","Company":"Chicken and co","Email":"john@chickenandco.com","BirthDate":"0001-01-01T00:00:00","PhoneNumberProfessional":"3123123123","PhoneNumberPersonal":null,"Address":null}'

Update record
curl -X PUT \
  http://localhost:1234/api/values/id \
  -H 'Cache-Control: no-cache' \
  -H 'Content-Type: application/json' \
  -H 'Postman-Token: e7011aa5-6b48-4f77-ac5a-17f7ae3485b5' \
  -d '{"Id":1,"Name":"Jane Doe","Company":"","Email":"john@chickenandco.com","BirthDate":"0001-01-01T00:00:00","PhoneNumberProfessional":null,"PhoneNumberPersonal":null,"Address":null}'

Delete Record
curl -X DELETE \
  http://localhost:1234/api/values/7 \
  -H 'Cache-Control: no-cache' \
  -H 'Content-Type: application/json' \
  -H 'Postman-Token: 2161e704-6bf0-4743-95b2-3f7abbe52c05' \
  -d '{"Name":"John Doe","Company":"Chicken and co","Email":"john@chickenandco.com","BirthDate":"0001-01-01T00:00:00","PhoneNumberProfessional":null,"PhoneNumberPersonal":null,"Address":null}'