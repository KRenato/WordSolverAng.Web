import { LetterPattern, LetterPatternValue } from './letter-pattern';

describe('LetterPattern', () => {
  let letterPattern: LetterPattern;

  beforeEach(() => {
    letterPattern = new LetterPattern();
  });

  it('should create an instance', () => {
    expect(letterPattern).toBeTruthy();
  });

  it('should default value to LetterPatternValue.DoesNotMatch', () => {
    expect(letterPattern.value).toEqual(LetterPatternValue.DoesNotMatch);
  });

  it('should toggle value from LetterPatternValue.DoesNotMatch to LetterPatternValue.WordMatch', () => {
    letterPattern.togglePattern();
    expect(letterPattern.value).toEqual(LetterPatternValue.WordMatch);
  });

  it('should toggle value from LetterPatternValue.WordMatch to LetterPatternValue.ExactMatch', () => {
    letterPattern.togglePattern();
    letterPattern.togglePattern();
    expect(letterPattern.value).toEqual(LetterPatternValue.ExactMatch);
  });

  it('should toggle value from LetterPatternValue.ExactMatch to LetterPatternValue.DoesNotMatch', () => {
    letterPattern.togglePattern();
    letterPattern.togglePattern();
    letterPattern.togglePattern();
    expect(letterPattern.value).toEqual(LetterPatternValue.DoesNotMatch);
  });

  it('should expect getStyle() to return "color-box color-box-grey" when value = LetterPatternValue.DoesNotMatch', () => {
    expect(
      letterPattern.value === LetterPatternValue.DoesNotMatch &&
        letterPattern.getStyle() === 'color-box color-box-grey'
    ).toBeTrue();
  });

  it('should expect getStyle() to return "color-box color-box-yellow" when value = LetterPatternValue.WordMatch', () => {
    letterPattern.togglePattern();
    expect(
      letterPattern.value === LetterPatternValue.WordMatch &&
        letterPattern.getStyle() === 'color-box color-box-yellow'
    ).toBeTrue();
  });

  it('should expect getStyle() to return "color-box color-box-green" when value = LetterPatternValue.ExactMatch', () => {
    letterPattern.togglePattern();
    letterPattern.togglePattern();
    expect(
      letterPattern.value === LetterPatternValue.ExactMatch &&
        letterPattern.getStyle() === 'color-box color-box-green'
    ).toBeTrue();
  });
});
