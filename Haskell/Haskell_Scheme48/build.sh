#!/bin/bash

echo Building Scheme Parser
ghc -package parsec -o parser Main.hs SchemeParser.hs
