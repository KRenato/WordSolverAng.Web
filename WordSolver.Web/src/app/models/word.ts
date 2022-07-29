import { LetterPattern, LetterPatternValue } from './letter-pattern';

export class Word {
  value: string;
  letterPatterns: LetterPattern[] = [];

  constructor(word: string) {
    this.value = word;

    for (let i = 0; i < this.value.length; i++) {
      this.letterPatterns.push(new LetterPattern());
    }
  }

  isExactMatch(): boolean {
    return this.letterPatterns.every(
      p => p.value == LetterPatternValue.ExactMatch
    );
  }
}
