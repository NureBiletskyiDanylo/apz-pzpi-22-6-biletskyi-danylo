import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { AccountService } from '../_services/account.service';

export const adminGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);

  if (accountService.currentUser() && accountService.currentUser()?.role?.toLowerCase() === "admin"){
    return true;
  }
  return false;
};
