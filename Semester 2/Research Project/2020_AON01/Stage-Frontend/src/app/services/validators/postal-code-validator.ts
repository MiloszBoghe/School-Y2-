import { FormControl, ValidatorFn } from '@angular/forms';
import { isPostalCode } from 'validator';

export function requireIsPostalCodeValidator(): ValidatorFn {
  return function validate(formControl: FormControl) {
    if (!formControl.value) {
      return null;
    }

    if (
      !isPostalCode(formControl.value, 'BE') &&
      !isPostalCode(formControl.value, 'NL')
    ) {
      return {
        requirePostalCode: true,
      };
    }

    return null;
  };
}
