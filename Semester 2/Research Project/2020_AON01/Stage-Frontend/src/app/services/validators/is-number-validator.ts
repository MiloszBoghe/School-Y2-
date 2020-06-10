import { FormControl, ValidatorFn } from '@angular/forms';
import { isNumeric } from 'validator';

export function requireIsNumberValidator(): ValidatorFn {
  return function validate(formControl: FormControl) {
    if (!formControl.value) {
      return null;
    }

    if (!isNumeric(formControl.value)) {
      return {
        requireNumber: true,
      };
    }

    return null;
  };
}
