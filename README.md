# JaccardIndexTextComparator
Super inefficient text comparator based on Jaccard index with toggleable spellcheck

## What is it good for?
It's the most basic way to compare two texts and determine their similarety with a numerical value ranging from 0 to 1.

## Why is it inefficient?
It's written using the "WeCantSpell" package for monodeveloper. The more efficient way would be to use Nhunspell, but the provided packeges don't support copilation on my linux system. If you care enough about a fast spellcheck, you will have to rewrite the "Correct" methode to use propper Hunspell. If you don't care about misspellings and want a balzing fast comparison, just toggle the spellcheck variable in the "Analysis" class constructor.

## What wasn't tested yet
Foraign scipt, emoticons, immage embeddings. Basically I only tried plain UTF8 text.
