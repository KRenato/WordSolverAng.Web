export class LetterPattern {
  private readonly valueMap = new Map<LetterPatternValue, any>();

  value: LetterPatternValue = LetterPatternValue.DoesNotMatch;

  constructor() {
    this.value = LetterPatternValue.DoesNotMatch;

    this.valueMap.set(LetterPatternValue.DoesNotMatch, {
      nextPattern: LetterPatternValue.WordMatch,
      style: 'color-box color-box-grey',
    });

    this.valueMap.set(LetterPatternValue.WordMatch, {
      nextPattern: LetterPatternValue.ExactMatch,
      style: 'color-box color-box-yellow',
    });

    this.valueMap.set(LetterPatternValue.ExactMatch, {
      nextPattern: LetterPatternValue.DoesNotMatch,
      style: 'color-box color-box-green',
    });
  }

  togglePattern() {
    this.value = this.valueMap.get(this.value).nextPattern;
  }

  getStyle() {
    return this.valueMap.get(this.value).style;
  }
}

export enum LetterPatternValue {
  Unknown = 0,
  DoesNotMatch = 1,
  WordMatch = 2,
  ExactMatch = 3,
}
