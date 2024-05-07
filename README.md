# HoroscopeHavoc
The following is a .NET 4.7.2 Console Application that calls a horoscope API based on a user-entered zodiac.

This project helped facilitate the following things:
- Pushing of a .NET Git project directly from visual studio
- Using Configuration Builder this time to pull in a secret (non-standard)
- More error handling than I'm used to

Things I should probably do:
- [ ] Split this out, maybe another function outside of main to take input and another one to make the API call
- [ ] Better way to manage secrets
- [ ] Maybe using an enum for the IList of starSigns

# How to set up
1. Create a file next to the `Program.cs` called `appsettings.json`
2. Get an API key from https://rapidapi.com/soralapps/api/daily-horoscope-api
3. Input the API key given in the `appsettings.json`, setting it up in the following format:
```
{
    "ApiSettings": {
        "ApiKey": "3af219e-key-should-be-in-this-format-308e643"
    }
}
```
4. Run it
