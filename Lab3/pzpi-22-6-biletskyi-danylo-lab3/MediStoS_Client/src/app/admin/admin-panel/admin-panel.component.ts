import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Member } from '../../_models/member';
import { UserService } from '../../_services/user.service';
import { AccountService } from '../../_services/account.service';
import { DbService } from '../../_services/db.service';

@Component({
  selector: 'app-admin-panel',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-panel.component.html',
  styleUrl: './admin-panel.component.css'
})
export class AdminPanelComponent implements OnInit {
  private userService = inject(UserService);
  accountService = inject(AccountService);
  private toastr = inject(ToastrService);
  private dbService = inject(DbService);

  members: Member[] = [];
  isLoading = true;
  roles = ['WarehouseWorker', 'Manager', 'Admin', 'DBAdmin'];
  editedMember: Member | null = null;

  ngOnInit(): void {
    this.loadMembers();
  }

loadMembers(): void {
  this.userService.getMembers().subscribe({
    next: (members) => {
      // Ensure roles are properly mapped
      this.members = members.map(member => ({
        ...member,
        role: member.role || 'WarehouseWorker' // Default if role is missing
      }));
      console.log('Members loaded:', this.members);
      this.isLoading = false;
    },
    error: () => {
      this.toastr.error('Failed to load members');
      this.isLoading = false;
    }
  });
}

  startEdit(member: Member): void {
    // This would navigate to a separate user-edit component in a real app
    // For now, we'll keep the inline edit but only for non-role fields
    this.editedMember = { ...member };
  }

  cancelEdit(): void {
    this.editedMember = null;
  }

  saveChanges(): void {
    if (!this.editedMember) return;

    this.userService.updateUser(this.editedMember).subscribe({
      next: () => {
        console.log('Member updated:', this.editedMember);
        this.toastr.success('Member updated successfully');
        this.loadMembers();
        this.editedMember = null;
      },
      error: () => {
        this.toastr.error('Failed to update member');
      }
    });
  }

  deleteMember(id: number): void {
    if (confirm('Are you sure you want to delete this member?')) {
      this.userService.deleteUser(id).subscribe({
        next: () => {
          this.toastr.success('Member deleted successfully');
          this.loadMembers();
        },
        error: () => {
          this.toastr.error('Failed to delete member');
        }
      });
    }
  }

onRoleChange(member: Member, event: Event): void {
  const selectElement = event.target as HTMLSelectElement;
  const newRole = selectElement.value;
  console.log(`Changing role for member ${member.id} to ${newRole}`);
  this.changeRole(member, newRole);
}

  changeRole(member: Member, newRole: string): void {
    console.log(member);
    console.log(newRole);
    if (member.role === newRole) return;
    console.log(`Updating role for member ${member.id} to ${newRole}`);
    this.userService.updateUserRole(member.id, newRole).subscribe({
      next: () => {
        this.toastr.success('Role updated successfully');
        // Update local state to avoid full reload if desired
        member.role = newRole;
      },
      error: () => {
        this.toastr.error('Failed to update role');
      }
    });
  }

  backup(){
    this.dbService.backup().subscribe({
      next: () => {
        this.toastr.success('Database backup created successfully');
      },
      error: () => {
        this.toastr.error('Failed to create database backup');
      }});
  }

  restore(){
    this.dbService.restore().subscribe({
      next: () => {
        this.toastr.success('Database restore done successfully');
      },
      error: () => {
        this.toastr.error('Failed to restore database');
      }});
  }
}
