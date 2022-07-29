import { Word } from './word';

describe('Word', () => {
  let word: Word;

  beforeEach(() => {
    word = new Word('arose');
  });

  it('should create an instance', () => {
    expect(word).toBeTruthy();
  });

  it('should expect isExactMatch() to return false by default', () => {
    expect(word.isExactMatch()).toBeFalse();
  });

  it('should expect isExactMatch() to return true when all LetterPattern values = LetterPatternValue.ExactMatch', () => {
    word.letterPatterns.forEach(p => {
      p.togglePattern();
      p.togglePattern();
    });
    expect(word.isExactMatch()).toBeTrue();
  });

  it('should have the same number of LetterPatterns as characters in string value', () => {
    expect(word.isExactMatch()).toBeFalse();
  });
});
