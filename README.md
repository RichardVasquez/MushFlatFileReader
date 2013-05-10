TinyMUSH Flat File Reader
=========================

This reads your standard TinyMUSH 3.X ASCII flat file and turns it into a JSON file.  The JSON file returns **everything** _(at least what I could figure out)_ about an object.  Related DbRefs, flags, attributes, and more.

This was written for some personal research I'm doing, so it has a few hardcoded options in it, and it doesn't deal with every single edge case that might be designated in your game's configuration.  Additionally, it looks for the ASCII file to be named "flatfile.txt" and it outputs "tinymush.json".

It's pretty fast though, and I could likely make it faster if I cached some of the parsers, but it processes the old Nowhere MUSH database in about 28 seconds on my machine, so I can live with that.

I'm throwing this out there as proof of concept, and as a jumping off point for others who might be able to use it.  Feel free to send me pull requests.

---

Here's an example result:

```JSON
[
  {
    "Flags": [
      "Dark",
      "HasCommands",
      "Dirty"
    ],
    "Powers": [],
    "Attributes": [
      {
        "Name": "DESC",
        "Id": 6,
        "Owner": 3,
        "Text": "%tYou are in the center of a vast field of foggy white. There is no visible light source, yet everything is illuminated.%r%tParticles, felt though invisible, bump into you, pushing you to and fro in an orgy of brownian motion.%r%r%tWhen you are ready to enter this not so brave, new world, type \\\"START\\\" (Without the quotes.)",
        "Flags": []
      },
      {
        "Name": "XDENSITY",
        "Id": 43608,
        "Owner": 3,
        "Text": "152",
        "Flags": [
          "Wizard",
          "Mdark"
        ]
      },
      {
        "Name": "ADJACENT",
        "Id": 46979,
        "Owner": 1,
        "Text": "#2 #-2",
        "Flags": []
      }
    ],
    "DbRef": 0,
    "Location": -1,
    "Zone": -1,
    "Contents": 232,
    "Exits": 1515,
    "Link": -1,
    "Next": -1,
    "Owner": 3,
    "Parent": -1,
    "Money": 0,
    "AccessTime": "2005-09-05T13:04:55",
    "ModTime": "2004-12-27T18:07:20",
    "LockKey": null,
    "Name": "Nowhere",
    "ObjectType": "Room"
  }
]

```

Obviously, there'd be a lot more than that in your final output file.

---

Requirements: 

 * [Sprache](https://www.nuget.org/packages/Sprache/)
 * [Json.NET](https://www.nuget.org/packages/Newtonsoft.Json/)
 * .NET 4.0
