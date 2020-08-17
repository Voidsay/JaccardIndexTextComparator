# JaccardIndexTextComparator
Super inefficient text comparator based on Jaccard index with toggleable spellcheck

# If you came here from itch.io
Please open the "CUI_Score.pdf" file there you can find the full list of scores as well as the ranked list. I am sort of missusing this github to host it here, since it is sort of related to this project.

## What is it good for?
It's the most basic way to compare two texts and determine their similarety with a numerical value ranging from 0 to 1.

## Why is it inefficient?
It's written using the "WeCantSpell" package for monodeveloper. The more efficient way would be to use Nhunspell, but the provided packeges don't support copilation on my linux system. If you care enough about a fast spellcheck, you will have to rewrite the "Correct" methode to use propper Hunspell. If you don't care about misspellings and want a balzing fast comparison, just toggle the spellcheck variable in the "Analysis" class constructor.

## What wasn't tested yet?
Foraign scipt, emoticons, immage embeddings. Basically I only tried plain UTF8 text.

## What do I need to compile the code?
In addition to the sourcecode you will need to download https://github.com/aarondandy/WeCantSpell.Hunspell or an equivalent package for your compiler. If you don't need spellchecking, just remove the not working code bits and you'll be golden
