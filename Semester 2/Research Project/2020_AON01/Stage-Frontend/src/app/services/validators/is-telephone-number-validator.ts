import { FormControl, ValidatorFn } from '@angular/forms';
import { isMobilePhone } from 'validator';

export function requireIsTelephoneNumberValidator(): ValidatorFn {
  return function validate(formControl: FormControl) {
    if (!formControl.value) {
      return null;
    }

    if (!isMobilePhone(formControl.value, ['nl-BE', 'nl-NL'])) {
      return {
        requireTelephoneNumber: true,
      };
    }

    return null;
  };
}
