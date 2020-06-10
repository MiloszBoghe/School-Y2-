import { FormGroup, ValidatorFn } from '@angular/forms';

export function requirePasswordsToBeTheSameValidator(
  firstField: string,
  secondField: string
): ValidatorFn {
  return function validate(
    formGroup: FormGroup
  ): { [key: string]: boolean } | null {
    if (
      !formGroup.get(firstField).value ||
      !formGroup.get(secondField).value ||
      (formGroup.errors && !formGroup.errors.requirePasswordsToBeTheSame)
    ) {
      return null;
    }

    if (formGroup.get(firstField).value !== formGroup.get(secondField).value) {
      return { requirePasswordsToBeTheSame: true };
    }
    return null;
  };
}
