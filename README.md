### How to Test the Fine Notification System

This guide explains how to test the real-time notification feature by having a **Security User** issue a fine and a **Regular User** receive the notification instantly.

#### User Roles for Testing

1.  **Security User (Sender):** The account responsible for creating fines and triggering notifications.
    -   **Email:** `security@parking.com`
    -   **Password:** `Password123!`

2.  **Regular User (Receiver):** Any standard registered user who will receive the fine notification. You will need to create this user yourself.

---

### Testing Steps

1.  **Create and Log In as a Regular User**
    -   Launch the application.
    -   Navigate to the **Register** page and create a new user account (e.g., `user@parking.com` with password `Password123!`).
    -   Log in with this new account in your primary browser (e.g., Google Chrome).
    -   **Keep this browser window open.** This user is now actively listening for new notifications.

2.  **Log In as the Security User**
    -   Open a **different browser** (e.g., Firefox) or an **Incognito/Private Window**. This is essential to maintain two separate user sessions.
    -   Navigate to the application's URL.
    -   Log in using the security account's credentials:
        -   **Email:** `security@parking.com`
        -   **Password:** `Password123!`

3.  **Issue a New Fine**
    -   In the browser where you are logged in as the **security user**, navigate to the page for adding new fines or violations.
    -   Create a new fine, ensuring you assign it to the regular user you created in Step 1 (`user@parking.com`).
    -   Save or submit the fine.

4.  **Verify the Notification Receipt**
    -   **Immediately**, switch back to the first browser window where the **regular user** (`user@parking.com`) is logged in.
    -   A toast notification should appear on the screen, informing the user that a new fine has been issued against them.
